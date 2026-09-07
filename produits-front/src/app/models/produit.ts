
export interface Produit
{
    id: number;
    nom: string;
    description: string;
    prix: number;
    stock: number;
    dateCreation: string;
}

export interface ProduitCreate
{
    nom: string;
    description: string;
    prix: number;
    stock : number;
}
