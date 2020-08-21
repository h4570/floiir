import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterComponent } from './register.component';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { InvitationKeyService } from 'src/app/services/invitation-key.service';
import { UserHttpService } from 'src/app/services/http/user.http.service';

@NgModule({
  declarations: [
    RegisterComponent
  ],
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    MatButtonModule
  ],
  providers: [
    InvitationKeyService,
    UserHttpService
  ]
})
export class RegisterModule { }
