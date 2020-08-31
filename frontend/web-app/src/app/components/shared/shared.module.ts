import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialogModule } from '@angular/material/dialog';
import { FastDialogService } from './../../services/fast-dialog.service';
import { TranslateModule } from '@ngx-translate/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { FastDialogComponent } from './fast-dialog/fast-dialog.component';

@NgModule({
    declarations: [
        FastDialogComponent,
    ],
    imports: [
        CommonModule,
        MatDialogModule,
        TranslateModule,
        MatButtonModule,
        MatIconModule,
    ],
    exports: [
    ],
    providers: [
        FastDialogService,
    ],
    entryComponents: [
        FastDialogComponent,
    ]
})
export class SharedModule { }
