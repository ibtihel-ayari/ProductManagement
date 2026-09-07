import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Produit } from '../models/produit';
import { ProduitService } from '../services/produit.service';

@Component({
  selector: 'app-produits-list',
  standalone: true,
  imports: [CommonModule, RouterLink],   // ce dont le template a besoin
  templateUrl: './produits-list.html',
})
export class ProduitsList implements OnInit {
  produits: Produit[] = [];
  chargement = true;
  erreur = '';

  // Le service est injecté dans le constructeur
  constructor(private produitService: ProduitService) {}

  // ngOnInit s'exécute automatiquement au chargement du composant
  ngOnInit(): void {
    this.chargerProduits();
  }

  chargerProduits(): void {
    this.chargement = true;
    // On s'ABONNE à l'Observable renvoyé par le service
    this.produitService.getAll().subscribe({
      next: (data) => {           // en cas de succès
        this.produits = data;
        this.chargement = false;
      },
      error: (err) => {           // en cas d'erreur
        this.erreur = 'Impossible de charger les produits.';
        this.chargement = false;
        console.error(err);
      }
    });
  }

  supprimer(id: number): void {
    if (!confirm('Supprimer ce produit ?')) return;
    this.produitService.delete(id).subscribe({
      next: () => this.chargerProduits(),   // on recharge la liste
      error: (err) => console.error(err)
    });
  }
}