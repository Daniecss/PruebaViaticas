import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AsignarPeliculaSalaComponent } from './asignarpeliculasala.component';


describe('AsignarpeliculasalaComponent', () => {
  let component: AsignarPeliculaSalaComponent;
  let fixture: ComponentFixture<AsignarPeliculaSalaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AsignarPeliculaSalaComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AsignarPeliculaSalaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
