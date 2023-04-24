import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { DashboardComponent } from './dashboard/dashboard.component';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './shared/navbar.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { httpInterceptorProviders } from './core/interceptors/http.interceptor';
import { FooterComponent } from './shared/footer.component';
import { SidebarComponent } from './shared/sidebar.component';
import { CompanyComponent } from './company-entity/company-entity.component';
import { CompaniesOverviewComponent } from './companies-overview/companies-overview.component';
import { ServicesOverviewComponent } from './services-overview/services-overview.component';
import { UsersOverviewComponent } from './users-overview/users-overview.component';
import { ServiceComponent } from './service-entity/service-entity.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { AddCompanyComponent } from './add-company/add-company.component';
import { HomeOverviewComponent } from './home-overview/home-overview.component';
import { CompanyListComponent } from './company-list/company-list.component';
import { ServiceListComponent } from './service-list/service-list.component';
import { UserListComponent } from './user-list/user-list.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    NavbarComponent,
    SidebarComponent,
    FooterComponent,
    CompanyComponent,
    ServiceComponent,
    CompaniesOverviewComponent,
    ServicesOverviewComponent,
    UsersOverviewComponent,
    PageNotFoundComponent,
    AddCompanyComponent,
    HomeOverviewComponent,
    CompanyListComponent,
    ServiceListComponent,
    UserListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [httpInterceptorProviders],
  bootstrap: [AppComponent]
})
export class AppModule { }
