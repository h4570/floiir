import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { InvitationKeyService } from 'src/app/services/invitation-key.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private invKeyService: InvitationKeyService
  ) { }

  public async ngOnInit(): Promise<void> {
    const key = this.route.snapshot.paramMap.get('key');
    await this.invKeyService.init(key);
  }

}
