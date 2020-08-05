import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { AppHistoryService } from 'src/app/services/app-history.service';
import { TranslateModule } from '@ngx-translate/core';
import { SharedModule } from '../../shared/shared.module';

@NgModule({
    declarations: [
        HomeComponent
    ],
    imports: [
        CommonModule,
        TranslateModule,
        SharedModule,
    ],
    exports: [
        HomeComponent
    ],
    providers: [
        AppHistoryService
    ]
})
export class HomeModule { }
