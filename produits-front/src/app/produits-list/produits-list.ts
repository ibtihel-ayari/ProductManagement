import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
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
  constructor(
  private produitService: ProduitService,
  private cdr: ChangeDetectorRef        // ← ajoute ceci
) {}

  // ngOnInit s'exécute automatiquement au chargement du composant
  ngOnInit(): void {
  console.log('✅ ngOnInit exécuté');
  this.chargerProduits();
}

chargerProduits(): void {
  this.chargement = true;
  this.produitService.getAll().subscribe({
    next: (data) => {
      this.produits = data;
      this.chargement = false;
      this.cdr.detectChanges();          // ← force le re-rendu
    },
    error: (err) => { this.erreur = 'Impossible de charger les produits.'; this.chargement = false; this.cdr.detectChanges(); }
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