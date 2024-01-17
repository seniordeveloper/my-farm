import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AnimalRouterComponent } from "./animal.router.component";
import { AnimalRoutes } from "src/app/common/routes";
import { AnimalListComponent } from "./components/animal-list/animal-list.component";

const routes: Routes = [
    {
        path: "",
        component: AnimalRouterComponent,
        children: [
            {
                path: "",
                redirectTo: AnimalRoutes.Animals,
                pathMatch: "full"
            },
            {
                path: AnimalRoutes.Animals,
                component: AnimalListComponent
            },
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AnimalRoutingModule { }
