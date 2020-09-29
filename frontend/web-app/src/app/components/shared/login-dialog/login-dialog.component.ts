import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { AuthService, LoginResponse } from 'src/app/services/auth.service';
import { environment } from 'src/environments/environment';


@Component({
  selector: 'app-login-dialog',
  templateUrl: './login-dialog.component.html',
  styleUrls: ['./login-dialog.component.scss']
})

export class LoginDialogComponent {


  // Data
  public reCaptchaKey: string = environment.reCaptchaKey;
  private reCaptchaToken: string;

  // Form
  public loginForm = new FormGroup({
    login: new FormControl(''),
    password: new FormControl(''),
  });


  constructor(
    public readonly dialogRef: MatDialogRef<LoginDialogComponent>,
    public readonly translate: TranslateService,
    private readonly authService: AuthService,
    private readonly router: Router
  ) { }


  /** Triggered when user will click on Submit button.
   * Properties trimming
   * Form & reCaptcha validation
   * On success: redirects to home and set that user is logged in authservice
   * On fail: shows dialog with error information
   */
  public async onSubmit(): Promise<void> {
    this.trimForm();
    console.log('1');

    if (!this.loginForm.invalid) {
      const loginRequest = await this.authService.login(
        this.reCaptchaToken,
        this.loginForm.controls.login.value,
        this.loginForm.controls.password.value
      );
      if (loginRequest === LoginResponse.Success) {
        this.dialogRef.close();
        this.router.navigateByUrl('/map');
      }
    }
  }

  /** Triggered when user will resolve reCaptcha challenge */
  public onReCaptchaResolve(token: string): void { this.reCaptchaToken = token; }

  /** Helper method for trimming form properties */
  private trimForm(): void {
    this.loginForm.controls.login.setValue(this.loginForm.controls.login.value.trim());
    this.loginForm.controls.password.setValue(this.loginForm.controls.password.value.trim());
  }
}
