import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialogModule } from '@angular/material/dialog';
import { FastDialogService } from './../../services/fast-dialog.service';
import { TranslateModule } from '@ngx-translate/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { FastDialogComponent } from './fast-dialog/fast-dialog.component';
import { LoginDialogComponent } from './login-dialog/login-dialog.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatInputModule } from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { RecaptchaFormsModule, RecaptchaModule } from 'ng-recaptcha';

@NgModule({
    declarations: [
        FastDialogComponent,
        LoginDialogComponent
    ],
    imports: [
        CommonModule,
        MatDialogModule,
        TranslateModule,
        MatButtonModule,
        MatIconModule,
        MatSnackBarModule,
        MatInputModule,
        ReactiveFormsModule,
        MatProgressBarModule,
        MatButtonModule,
        RecaptchaModule,
        TranslateModule,
        RecaptchaFormsModule
    ],
    exports: [
    ],
    providers: [
        FastDialogService,
    ],
    entryComponents: [
        FastDialogComponent,
        LoginDialogComponent
    ]
})
export class SharedModule { }
