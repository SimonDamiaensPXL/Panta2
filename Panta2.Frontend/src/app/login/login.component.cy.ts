import { HttpClientTestingModule } from '@angular/common/http/testing';
import { StorageService } from "../core/services/storage/storage.service";
import { AuthService } from "../core/services/auth/auth.service";
import { LoginComponent } from "./login.component";
import { FormsModule } from "@angular/forms";
import { Router } from "@angular/router";
import { HttpClient } from '@angular/common/http';

describe('LoginComponent', () => {
    beforeEach(async () => {
        cy.mount(LoginComponent, {
            declarations: [LoginComponent],
            imports: [
                HttpClientTestingModule,
                FormsModule,
            ],
            providers: [
                AuthService,
                StorageService,
                Router,
                HttpClient
            ]
        });
    });

    it('should display login form', () => {
        cy.get('input[name="username"]').should('exist');
        cy.get('input[name="password"]').should('exist');
        cy.get('button').should('exist');
    });

    it('should navigate to home on successful login', () => {
        cy.intercept('POST', '/api/login', { fixture: 'login-success.json' });
        cy.get('input[name="username"]').type('valid');
        cy.get('input[name="password"]').type('password');
        cy.get('button').click();
    });
});  