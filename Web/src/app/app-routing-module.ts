import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import path from 'path';
import { ProductListComponent } from './product-list/product-list.component';
import { LoginComponent } from './login/login.component';
import { FavoritesComponent } from './favorites/favorites.component';
import { RegisterComponent } from './register/register.component';
import { BasketComponent } from './basket/basket.component';

const routes: Routes = [
  {
    path: '',
    component: ProductListComponent
  },
  {
    path: 'login', component: LoginComponent
  },
  {
    path: 'favorites', component: FavoritesComponent
  },
  {
    path: 'register', component: RegisterComponent
  },
  {
    path:'basket', component: BasketComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
