import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterComponent } from './register.component';
import { InvitationKeyService } from 'src/app/services/invitation-key.service';

@NgModule({
  declarations: [
    RegisterComponent
  ],
  imports: [
    CommonModule
  ],
  providers: [
    InvitationKeyService
  ]
})
export class RegisterModule { }
