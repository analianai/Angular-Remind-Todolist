import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LazyLoadComponent } from './lazy-load.component';

describe('LazyLoadComponent', () => {
  let component: LazyLoadComponent;
  let fixture: ComponentFixture<LazyLoadComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [LazyLoadComponent]
    });
    fixture = TestBed.createComponent(LazyLoadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
