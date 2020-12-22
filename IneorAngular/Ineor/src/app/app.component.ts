import { Component, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

@Injectable()
export class AppComponent {
  title = 'Ineor';
  countryCode = null;
  showLowestVat = false;
  showCountry = false;
  showHighestVat = false;
  placeDetail = null;
  arrayOfCountries = null;

  constructor(private http: HttpClient) {
  }

  lowestVat = () =>{
    this.http.get("https://localhost:44368/Rates/lowestVat")
      .subscribe((data:any) => {
        this.arrayOfCountries = data;
      });
    this.showLowestVat = !this.showLowestVat;
    this.showCountry = false;
    this.showHighestVat = false;
  }

  highestVat = () =>{
    this.http.get("https://localhost:44368/Rates/highestVat")
    .subscribe((data:any) => {
      this.arrayOfCountries = data;
    });
    
    this.showLowestVat = false;
    this.showCountry = false;
    this.showHighestVat = !this.showHighestVat;
  }

  country = () =>{
    let countryCode = window.prompt("What country do you want to see? Write country code. \nExample: Slovenia - SI, Austria - AT", "");
    this.http.post(`https://localhost:44368/Rates/countryVat/${countryCode}`, null)
    .subscribe((data:any) => {
      this.placeDetail = data;
    });

    this.showLowestVat = false;
    this.showCountry = true;
    this.showHighestVat = false;
  }
}
