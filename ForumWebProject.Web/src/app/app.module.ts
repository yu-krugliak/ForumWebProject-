import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule, Provider, forwardRef } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import HomeComponent from './home/home.component';
import { AboutComponent } from './about/about.component';
import { PostListComponent } from './post-list/post-list.component';

import { MaterialModule } from './material.module';

import { ApiModule } from './api/api.module';
import { ApiInterceptor } from './shared/api-interceptor.service';
import { LoginFormComponent } from './login-form/login-form.component';
import { RegisterFormComponent } from './register-form/register-form.component';
import { WelcomeDialogComponent } from './welcome-dialog/welcome-dialog.component'
import { MatDialogRef } from '@angular/material/dialog';
import { PermissionsManager } from './Services/permissions-service';

export const API_INTERCEPTOR_PROVIDER: Provider = {
  provide: HTTP_INTERCEPTORS,
  useExisting: forwardRef(() => ApiInterceptor),
  multi: true
};

@NgModule({
  declarations: [
    AppComponent,
    
    HomeComponent,
    AboutComponent,
    LoginFormComponent,
    RegisterFormComponent,
    PostListComponent,
    WelcomeDialogComponent
  ],

  imports: [
    AppRoutingModule,
    HttpClientModule,
    ApiModule.forRoot({rootUrl: 'http://localhost:15891'}),

    BrowserModule, ReactiveFormsModule, MaterialModule, BrowserAnimationsModule,
  ],
  
  providers: [
    ApiInterceptor,
    API_INTERCEPTOR_PROVIDER,
    {
      provide: MatDialogRef,
      useValue: {}
    },
    PermissionsManager
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }