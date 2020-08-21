import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { InvitationKeyService } from 'src/app/services/invitation-key.service';
import { InvitationKeyManager } from './invitation-key-manager';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, ValidationErrors } from '@angular/forms';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  constructor(
    private readonly route: ActivatedRoute,
    private readonly http: HttpClient,
    private readonly invKeyService: InvitationKeyService
  ) {
    this.invKeyManager = new InvitationKeyManager(this.invKeyService);
  }

  // Managers
  public invKeyManager: InvitationKeyManager;

  // Data
  public terms: string;

  // Form
  public registerForm = new FormGroup({
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    login: new FormControl(''),
    password: new FormControl(''),
    confirmPassword: new FormControl(''),
    email: new FormControl(''),
  }, { validators: [this.passwordsValidator] });

  public async ngOnInit(): Promise<void> {
    this.terms = await this.getAppTerms();
    const key = this.route.snapshot.paramMap.get('key');
    await this.invKeyManager.init(key);
  }

  public passwordsValidator(registerForm: FormGroup): ValidationErrors {
    const pass = registerForm.get('password');
    const confirmPass = registerForm.get('confirmPassword');
    if (pass.errors || confirmPass.errors) return null;
    if (pass.value === confirmPass.value) confirmPass.setErrors(null);
    else confirmPass.setErrors({ passwordsMismatch: true });
    return null;
  }

  public async onSubmit(): Promise<void> {
    // TODO: Use EventEmitter with form value
    console.warn(this.registerForm.value);
  }

  private async getAppTerms(): Promise<string> {
    return this.http
      .get<string>('assets/terms.txt', { responseType: 'text' as 'json' })
      .toPromise()
      .then(result => result as any as string);
  }

}
