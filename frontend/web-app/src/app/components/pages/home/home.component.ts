import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { AppHistoryService } from 'src/app/services/app-history.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})

export class HomeComponent implements OnInit {

  constructor(
    private readonly translate: TranslateService,
    private readonly appHistoryService: AppHistoryService
  ) { }

  public async ngOnInit(): Promise<any> {
    const result = await this.appHistoryService.get(1);
    console.log(result);
  }

}
