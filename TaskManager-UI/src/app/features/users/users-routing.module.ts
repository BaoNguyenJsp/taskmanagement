import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { UserList } from "./pages/user-list/user-list";
import { UserDetail } from "./pages/user-detail/user-detail";

const routes: Routes = [
  { path: '', component: UserList },
  { path: ':id', component: UserDetail }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule {}