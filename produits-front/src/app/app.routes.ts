import { Routes } from '@angular/router';
import { ProduitsList } from './produits-list/produits-list';
import { ProduitForm } from './produit-form/produit-form';

export const routes: Routes = [
  { path: '', component: ProduitsList },              // page d'accueil
  { path: 'nouveau', component: ProduitForm },        // créer
  { path: 'modifier/:id', component: ProduitForm },   // modifier (param :id)
  { path: '**', redirectTo: '' },                              // route inconnue
];