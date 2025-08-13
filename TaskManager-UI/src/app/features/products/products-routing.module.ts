import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ProductList } from "./pages/product-list/product-list";
import { ProductDetail } from "./pages/product-detail/product-detail";

const routes: Routes = [
  { path: '', component: ProductList },
  { path: ':id', component: ProductDetail }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductsRoutingModule {}