import { AbstractControl, FormGroup, FormControl } from '@angular/forms';

export class MyValidators {
    static checkPass( control: FormGroup ) {
        return (control.root.value['contrasena'] === control.value) ? null : { noMatch: true };
    }
}