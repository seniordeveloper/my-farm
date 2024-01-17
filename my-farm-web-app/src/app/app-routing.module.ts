import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AnimalRoutes } from './common/routes';
import { NotFoundComponent } from './modules/shared/components/not-found/not-found.component';

const routes: Routes = [
    { path: "", redirectTo: AnimalRoutes.Root, pathMatch: "full" },
    {
        path: AnimalRoutes.Root,
        loadChildren: () =>
            import("./modules/animal/animal.module").then((m) => m.AnimalModule)
    },
    {
        path: "**",
        component: NotFoundComponent
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
