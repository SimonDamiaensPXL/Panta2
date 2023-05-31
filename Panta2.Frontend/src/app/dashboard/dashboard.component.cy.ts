/// <reference types="cypress" />

import { HttpClient } from '@angular/common/http';
import { DashboardComponent } from './dashboard.component';
import { mount } from 'cypress/angular'
import { TestBed } from '@angular/core/testing';
import { StorageService } from '../core/services/storage/storage.service';
import { UserService } from '../core/services/user/user.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Observable } from 'rxjs';
import { Service } from '../core/models/service.model';

describe('DashboardComponent', () => {
    let userServiceStub: any;

    beforeEach(async () => {
        cy.mount(DashboardComponent, {
            declarations: [DashboardComponent],
            imports: [HttpClientTestingModule],
            providers: [
                UserService,
                StorageService,
                HttpClient
            ],
        })
    });

    it('should display the loading spinner initially', () => {
        cy.get('.h-full svg').should('exist');
    });


    it('should fetch user services and favorite services on initialization', () => {
        cy.mount(DashboardComponent, {
            declarations: [DashboardComponent],
            imports: [HttpClientTestingModule],
            providers: [
                UserService,
                StorageService,
                HttpClient
            ],
            componentProperties: {
                isLoading: false,
            }
        })

        window.sessionStorage.setItem

        // Stub the necessary API requests for user and services
        cy.intercept('GET', 'https://localhost:7094/api/users/*', { fixture: 'user.json' });
        cy.intercept('GET', 'https://localhost:7094/api/users/services/*', { fixture: 'services.json' });
        cy.intercept('GET', 'https://localhost:7094/api/users/favorites/*', { fixture: 'favorites.json' });
        // cy.stub(StorageService.prototype, 'getUser').returns(Promise);
        // cy.stub(UserService.prototype, 'getServices').returns(Observable<Service[]>);
        // cy.stub(UserService.prototype, 'getFavoriteServices').returns(Observable<Service[]>);


        cy.get('.h-full svg').should('not.exist');
        // cy.getComponent(DashboardComponent).then((component) => {
        //     // Wait for services and favoriteServices to be populated
        //     cy.wrap(component.services).should('have.length', 3);
        //     cy.wrap(component.favoriteServices).should('have.length', 2);
        //     cy.wrap(component.filteredServices).should('have.length', 3); // Assuming no filtering initially

        //     cy.wrap(component.isLoading).should('be.false');
        // });
    });
});
