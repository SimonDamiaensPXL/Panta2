import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { PageNotFoundComponent } from './shared/page-not-found.component';
import { ServicesOverviewComponent } from './service/services-overview/services-overview.component';
import { CompaniesOverviewComponent } from './company/companies-overview/companies-overview.component';
import { UsersOverviewComponent } from './user/users-overview/users-overview.component';
import { HomeOverviewComponent } from './home-overview/home-overview.component';
import { AddCompanyComponent } from './company/add-company/add-company.component';
import { AddServiceComponent } from './service/add-service/add-service.component';
import { AddUserComponent } from './user/add-user/add-user.component';
import { EditCompanyComponent } from './company/edit-company/edit-company.component';
import { EditServiceComponent } from './service/edit-service/edit-service.component';
import { AddRoleComponent } from './user/add-role/add-role.component';
import { EditCompanyServiceComponent } from './company/edit-company-service/edit-company-service.component';
import { AddCompanyServiceComponent } from './company/add-company-service/add-company-service.component';
import { EditUserComponent } from './user/edit-user/edit-user.component';
import { EditRoleComponent } from './user/edit-role/edit-role.component';

const routes: Routes = [
  { path: 'home', component: HomeOverviewComponent},
  { path: 'companies', component: CompaniesOverviewComponent},
  { path: 'services', component: ServicesOverviewComponent},
  { path: 'users', component: UsersOverviewComponent},
  { path: 'companies/add', component: AddCompanyComponent},
  { path: 'services/add', component: AddServiceComponent},
  { path: 'company/:id', component: EditCompanyComponent},
  { path: 'company/:id/add-user', component: AddUserComponent},
  { path: 'company/:id/add-role', component: AddRoleComponent},
  { path: 'company/:id/add-service', component: AddCompanyServiceComponent},
  { path: 'service/:id', component: EditServiceComponent},
  { path: 'company/:id/edit-service/:service-id', component: EditCompanyServiceComponent},
  { path: 'company/:id/edit-user/:user-id', component: EditUserComponent},
  { path: 'company/:id/edit-role/:role-id', component: EditRoleComponent},
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {path: '**', component: PageNotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    preloadingStrategy: PreloadAllModules
  })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
