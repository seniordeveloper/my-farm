import { Injectable } from "@angular/core";
import { NgForm } from "@angular/forms";
import { BaseApiComponent } from "./base-api-component";

@Injectable()
export abstract class BaseFormComponent<T> extends BaseApiComponent {
    form: NgForm;

    model = {} as T;

    protected constructor() {
        super();
    }

    isValid(): boolean {
        return this.form?.valid ?? true;
    }

    hasError(key: string, error: string = null): boolean {
        return (
            !this.isValid() &&
            (this.form.controls[key]?.dirty ||
                this.form.controls[key]?.touched) &&
            this.form.controls[key].hasError(error)
        );
    }

    protected markFormTouched(): void {
        Object.keys(this.form.controls).forEach((k) => {
            this.form.controls[k].markAsTouched();
        });
    }
}
