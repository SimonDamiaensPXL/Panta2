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
import { CompanyComponent } from './company/company-entity/company-entity.component';
import { CompaniesOverviewComponent } from './company/companies-overview/companies-overview.component';
import { ServicesOverviewComponent } from './service/services-overview/services-overview.component';
import { UsersOverviewComponent } from './user/users-overview/users-overview.component';
import { ServiceComponent } from './service/service-entity/service-entity.component';
import { PageNotFoundComponent } from './shared/page-not-found.component';
import { AddCompanyComponent } from './company/add-company/add-company.component';
import { HomeOverviewComponent } from './home-overview/home-overview.component';
import { CompanyListComponent } from './company/companies-list/companies-list.component';
import { ServicesListComponent } from './service/services-list/services-list.component';
import { UsersListComponent } from './user/user-list/user-list.component';
import { CompanyTableComponent } from './company/companies-table/companies-table.component';
import { ServicesTableComponent } from './service/services-table/services-table.component';
import { DragDirective } from './core/directives/drag.directive';
import { AddServiceComponent } from './service/add-service/add-service.component';
import { AddUserComponent } from './user/add-user/add-user.component';
import { EditCompanyComponent } from './company/edit-company/edit-company.component';
import { EditServiceComponent } from './service/edit-service/edit-service.component';
import { EditUserComponent } from './user/edit-user/edit-user.component';
import { AddRoleComponent } from './user/add-role/add-role.component';
import { EditCompanyServiceComponent } from './company//edit-company-service/edit-company-service.component';
import { AddCompanyServiceComponent } from './company/add-company-service/add-company-service.component';
import { EditRoleComponent } from './user/edit-role/edit-role.component';

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
    ServicesListComponent,
    UsersListComponent,
    CompanyTableComponent,
    ServicesTableComponent,
    DragDirective,
    AddServiceComponent,
    AddUserComponent,
    EditCompanyComponent,
    EditServiceComponent,
    EditUserComponent,
    AddRoleComponent,
    EditCompanyServiceComponent,
    AddCompanyServiceComponent,
    EditRoleComponent
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
