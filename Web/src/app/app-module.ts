import { NgModule, provideBrowserGlobalErrorListeners, provideZonelessChangeDetection } from '@angular/core';
import { BrowserModule, provideClientHydration, withEventReplay } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { AppHeaderComponent } from './app-header/app-header.component';
import { authInterceptor } from './services/auth.interceptor';
import { LoginComponent } from './login/login.component';
import { FavoritesComponent } from './favorites/favorites.component';
import { RegisterComponent } from './register/register.component';
import { BasketComponent } from './basket/basket.component';




@NgModule({
  declarations: [
    App
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AppHeaderComponent,
    LoginComponent,
    FavoritesComponent,
    RegisterComponent,
    BasketComponent
  ],
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZonelessChangeDetection(),
    provideClientHydration(withEventReplay()),
    provideHttpClient(
      withFetch(),
      withInterceptors([authInterceptor])
    )
  ],
  bootstrap: [App]
})
export class AppModule { }
