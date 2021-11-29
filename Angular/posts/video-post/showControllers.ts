import { Subject } from 'rxjs';

export class ShowControllers {
  show: Subject<boolean> = new Subject<boolean>();
}
