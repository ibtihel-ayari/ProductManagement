import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Produit, ProduitCreate } from '../models/produit';

// @Injectable rend ce service disponible pour l'injection de dépendances.
// providedIn: 'root' → une seule instance partagée dans toute l'appli.
@Injectable({ providedIn: 'root' })
export class ProduitService {

  // ⚠️ Adaptez le PORT à celui affiché par votre backend !
  private apiUrl = 'http://localhost:5215/api/produits';

  // HttpClient est INJECTÉ dans le constructeur 
  constructor(private http: HttpClient) {}

  // Chaque méthode renvoie un Observable (flux asynchrone)
  getAll(): Observable<Produit[]> {
    return this.http.get<Produit[]>(this.apiUrl);
  }

  getById(id: number): Observable<Produit> {
    return this.http.get<Produit>(`${this.apiUrl}/${id}`);
  }

  create(produit: ProduitCreate): Observable<Produit> {
    return this.http.post<Produit>(this.apiUrl, produit);
  }

  update(id: number, produit: ProduitCreate): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, produit);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}