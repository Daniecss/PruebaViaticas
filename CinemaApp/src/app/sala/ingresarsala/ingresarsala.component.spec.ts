import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IngresarsalaComponent } from './ingresarsala.component';

describe('IngresarsalaComponent', () => {
  let component: IngresarsalaComponent;
  let fixture: ComponentFixture<IngresarsalaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [IngresarsalaComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IngresarsalaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
