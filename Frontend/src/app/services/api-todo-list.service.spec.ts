import { TestBed } from '@angular/core/testing';

import { ApiTodoListService } from './api-todo-list.service';

describe('ApiTodoListService', () => {
  let service: ApiTodoListService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ApiTodoListService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
