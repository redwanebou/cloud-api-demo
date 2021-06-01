import { Merk } from './merk.model';
import { Model } from './model.model';
import { Person } from './person.model';

export interface Car
{
  id?: number;
  bouwjaar: number;
  brandstof: string;
  prijs: number;
  verkocht: boolean;
  merk: Merk;
  model: Model;
  person: Person;
}
