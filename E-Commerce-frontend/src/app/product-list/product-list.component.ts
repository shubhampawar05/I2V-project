import { Component, OnInit } from '@angular/core';
import { Product } from '../models/product';
import { ProductService } from '../services/product.service';
import { CartService } from './../services/cart.service'; // Import CartService
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; // Import FormsModule

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, FormsModule], // Add FormsModule here
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  isProductModalOpen = false;
  isCartModalOpen = false;
  isEditing = false;
  productForm: Product = { productID: 0, productName: '', price: 0,  };
  cartForm = { productID: 0, quantity: 1 };

  constructor(private productService: ProductService, private cartService: CartService) { }

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.productService.getProducts().subscribe(products => this.products = products);
  }

  deleteProduct(id: number): void {
    this.productService.deleteProduct(id).subscribe(() => this.loadProducts());
  }

  openAddProductModal(): void {
    this.isEditing = false;
    this.productForm = { productID: 0, productName: '', price: 0 };
    this.isProductModalOpen = true;
  }

  openEditProductModal(product: Product): void {
    this.isEditing = true;
    this.productForm = { ...product };
    this.isProductModalOpen = true;
  }

  closeProductModal(): void {
    this.isProductModalOpen = false;
  }

  addProduct(): void {
    this.productService.addProduct(this.productForm).subscribe(() => {
      this.loadProducts();
      this.closeProductModal();
    });
  }

  updateProduct(): void {
    this.productService.updateProduct(this.productForm.productID, this.productForm).subscribe(() => {
      this.loadProducts();
      this.closeProductModal();
    });
  }

  openAddToCartModal(product: Product): void {
    this.cartForm.productID = product.productID;
    this.isCartModalOpen = true;
  }

  closeCartModal(): void {
    this.isCartModalOpen = false;
  }

  addToCart(): void {
    this.cartService.addToCart(this.cartForm).subscribe(() => {
      this.closeCartModal();
    });
  }
}
