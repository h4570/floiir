import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/pages/home/home.component';
import { RegisterComponent } from './components/pages/register/register.component';
import { ConfirmEmailComponent } from './components/pages/confirm-email/confirm-email.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'register/:key', component: RegisterComponent },
  { path: 'confirm-email', component: ConfirmEmailComponent },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
