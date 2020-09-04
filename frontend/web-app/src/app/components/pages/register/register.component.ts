import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { InvitationKeyCheckerService } from 'src/app/services/invitation-key-checker.service';
import { FormGroup, FormControl, ValidationErrors } from '@angular/forms';
import { UserService, RegisterResponse } from 'src/app/services/user.service';
import { User } from 'src/app/models/user.model';
import { FastDialogService } from 'src/app/services/fast-dialog.service';
import { DialogType, DialogButtonType } from '../../shared/fast-dialog/fast-dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from 'src/app/services/auth.service';
import { environment } from 'src/environments/environment';
import { RecaptchaComponent } from 'ng-recaptcha';
import { TranslateService } from '@ngx-translate/core';
import { AppService } from 'src/app/services/app.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  constructor(
    private readonly route: ActivatedRoute,
    private readonly router: Router,
    private readonly appService: AppService,
    private readonly userService: UserService,
    private readonly authService: AuthService,
    private readonly snackBar: MatSnackBar,
    private readonly fDialogService: FastDialogService,
    private readonly translate: TranslateService,
    public invKeyService: InvitationKeyCheckerService
  ) { }

  @ViewChild('reCaptchaRef')
  private reCaptchaComponent: RecaptchaComponent;

  // Data
  public terms: string;
  public reCaptchaKey: string = environment.reCaptchaKey;
  private reCaptchaToken: string;

  // Controls
  public loading: boolean;

  // Form
  public registerForm = new FormGroup({
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    login: new FormControl(''),
    password: new FormControl(''),
    confirmPassword: new FormControl(''),
    email: new FormControl(''),
  }, { validators: [this.passwordsValidator] });

  // Funcs

  public async ngOnInit(): Promise<void> {
    const key = this.route.snapshot.paramMap.get('key');
    this.appService.appTerms.then(res => this.terms = res);
    this.invKeyService.init(key);
  }

  /** Set's error on confirmPassworrd control, when password and confirmPassword do not match */
  public passwordsValidator(registerForm: FormGroup): ValidationErrors {
    const pass = registerForm.get('password');
    const confirmPass = registerForm.get('confirmPassword');
    if (pass.errors || confirmPass.errors) return null;
    if (pass.value === confirmPass.value) confirmPass.setErrors(null);
    else confirmPass.setErrors({ passwordsMismatch: true });
    return null;
  }

  /** Triggered when user will resolve reCaptcha challenge */
  public onReCaptchaResolve(token: string): void { this.reCaptchaToken = token; }

  /** Triggered when user will click on Submit button.
   * Properties trimming
   * Form & reCaptcha validation
   * On success: redirects to confirm-email and save JWT token in localStorage
   * On fail: shows fast dialog with error information
   */
  public async onSubmit(): Promise<void> {
    this.trimForm();
    if (!this.registerForm.invalid && this.reCaptchaToken) {
      this.loading = true;
      const result =
        await this.userService.register(
          this.invKeyService.invitationKey.key,
          this.reCaptchaToken,
          this.formUser,
          this.authService.saveToken
        );
      if (result === RegisterResponse.Success)
        this.router.navigateByUrl('/confirm-email');
      else {
        this.reCaptchaToken = null;
        this.reCaptchaComponent.reset();
        const title = this.translate.instant('register.registerError');
        let text: string;
        let type = DialogType.Alert;
        switch (result) {
          case RegisterResponse.EmailIsUsed:
            text = this.translate.instant('register.emailUsed') as string;
            break;
          case RegisterResponse.LoginIsUsed:
            text = this.translate.instant('register.loginUsed') as string;
            break;
          default:
            text = this.translate.instant('register.criticalError') as string;
            type = DialogType.Error;
            break;
        }
        this.loading = false;
        await this.fDialogService.open(type, DialogButtonType.Ok, title, [text]);
      }
    } else {
      this.snackBar.open(this.translate.instant('register.checkAllFields') as string, 'OK', { duration: 5000 });
    }
  }

  /** Helper method to adapt form to user model */
  private get formUser(): User {
    return {
      id: 0,
      login: this.registerForm.controls.login.value,
      password: this.registerForm.controls.password.value,
      firstName: this.registerForm.controls.firstName.value,
      lastName: this.registerForm.controls.lastName.value,
      email: this.registerForm.controls.email.value,
      fullName: null
    };
  }

  /** Helper method for trimming form properties */
  private trimForm(): void {
    this.registerForm.controls.login.setValue(this.registerForm.controls.login.value.trim());
    this.registerForm.controls.password.setValue(this.registerForm.controls.password.value.trim());
    this.registerForm.controls.firstName.setValue(this.registerForm.controls.firstName.value.trim());
    this.registerForm.controls.lastName.setValue(this.registerForm.controls.lastName.value.trim());
    this.registerForm.controls.email.setValue(this.registerForm.controls.email.value.trim());
  }

}
