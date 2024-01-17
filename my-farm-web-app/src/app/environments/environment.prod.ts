import { StoreDevtoolsModule } from "@ngrx/store-devtools";

export const environment = {
    name: "local",
    production: true,
    apiUrl: "https://localhost:7029",
    version: 0,
    idleTimeoutInSeconds: 1800,
    tokenExpirationInSeconds: 60,
    devModules: [StoreDevtoolsModule.instrument({ maxAge: 50 })]
};
