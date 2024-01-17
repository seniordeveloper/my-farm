import { Injectable } from "@angular/core";

@Injectable()
export abstract class BaseComponent {
    emailPattern = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    composeRoute(
        part1: string,
        part2?: string,
        part3?: string,
        part4?: string
    ): string {
        return `/${part1}${part2 ? `/${part2}` : ""}${part3 ? `/${part3}` : ""
            }${part4 ? `/${part4}` : ""}`;
    }
}
