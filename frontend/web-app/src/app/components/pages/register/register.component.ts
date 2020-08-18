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
    private invitationKeyService: InvitationKeyService
  ) { }

  public async ngOnInit(): Promise<any> {
    const key = this.route.snapshot.paramMap.get('key');
    // if (key.length === 10) { // TODO: extension method
    const response = await this.invitationKeyService.get(key);
    console.log(response);
    // }
  }

}
