import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-todo-items',
  templateUrl: './todo.component.html'
})
export class TodoItemsComponent {
  public todoItems: TodoModel[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<TodoModel[]>(baseUrl + 'api/todo').subscribe(result => {
      this.todoItems = result;
    }, error => console.error(error));
  }
}

interface TodoModel {
  id: number;
  title: string;
  description: string;
  complete: boolean;
  date: Date;
  priority: string;
}
