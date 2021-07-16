import { ComponentFixture, TestBed } from '@angular/core/testing';
import { DniValidationComponent } from './dni-validation.component';

describe('DniValidationComponent', () => {
  let component: DniValidationComponent;
  let fixture: ComponentFixture<DniValidationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DniValidationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DniValidationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
