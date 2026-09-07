import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ProduitService } from '../services/produit.service';

@Component({
  selector: 'app-produit-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './produit-form.html',
})
export class ProduitForm implements OnInit {
  form!: FormGroup;
  modeEdition = false;
  produitId?: number;

  constructor(
    private fb: FormBuilder,            // pour construire le formulaire
    private produitService: ProduitService,
    private route: ActivatedRoute,     // pour lire le paramètre :id de l'URL
    private router: Router             // pour naviguer après enregistrement
  ) {}

  ngOnInit(): void {
    // Création du formulaire réactif avec ses validateurs
    this.form = this.fb.group({
      nom:         ['', [Validators.required, Validators.minLength(2)]],
      description: [''],
      prix:        [0, [Validators.required, Validators.min(0)]],
      stock:       [0, [Validators.required, Validators.min(0)]],
    });

    // Si l'URL contient un id → mode modification
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.modeEdition = true;
      this.produitId = +id;   // le + convertit string → number
      this.produitService.getById(this.produitId).subscribe(p => {
        // patchValue : pré-remplit le formulaire avec les données existantes
        this.form.patchValue({
          nom: p.nom,
          description: p.description,
          prix: p.prix,
          stock: p.stock,
        });
      });
    }
  }

  // Raccourci pratique pour le template
  get f() { return this.form.controls; }

  enregistrer(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();   // affiche toutes les erreurs
      return;
    }

    const donnees = this.form.value;

    if (this.modeEdition && this.produitId) {
      this.produitService.update(this.produitId, donnees).subscribe({
        next: () => this.router.navigate(['/']),
        error: (err) => console.error(err),
      });
    } else {
      this.produitService.create(donnees).subscribe({
        next: () => this.router.navigate(['/']),
        error: (err) => console.error(err),
      });
    }
  }
}