import { Component, Input, OnInit } from '@angular/core';
import { Agence } from '@models/api/agency';

@Component({
  selector: 'app-agency-template',
  templateUrl: './agency-template.component.html',
  styleUrls: ['./agency-template.component.scss'],
})
export class AgencyTemplateComponent implements OnInit {
  @Input() agency: Agence | undefined = {
    AgenceId: undefined,
    NumeroTelephone: undefined,
    Mail: undefined,
    Nom: undefined,
    Latitude: undefined,
    Longitude: undefined,
    DateInscription: undefined,
    IsBlocked: undefined,
    Adresse: undefined,
    Password: undefined,
    Username: undefined,
    Signalement: undefined,
  };

  constructor() {}

  ngOnInit(): void {}
}
