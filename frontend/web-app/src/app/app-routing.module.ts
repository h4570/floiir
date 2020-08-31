import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/pages/home/home.component';
import { RegisterComponent } from './components/pages/register/register.component';
import { ConfirmEmailComponent } from './components/pages/confirm-email/confirm-email.component';
import { AuthGuard } from './auth-guard';
import { MapComponent } from './components/pages/map/map.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'map', component: MapComponent },
  { path: 'register/:key', component: RegisterComponent },
  { path: 'confirm-email', component: ConfirmEmailComponent, canActivate: [AuthGuard] },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
