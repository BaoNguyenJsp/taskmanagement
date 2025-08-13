import { Component, signal } from '@angular/core';
import { User } from '../../models/user.model';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AgGridModule } from 'ag-grid-angular';
import { ColDef, GridOptions, GridReadyEvent, PaginationChangedEvent } from 'ag-grid-community';
import { ModuleRegistry, AllCommunityModule } from 'ag-grid-community';

// Register all community features once at app startup
ModuleRegistry.registerModules([AllCommunityModule]);

@Component({
  selector: 'app-user-list',
  imports: [CommonModule, AgGridModule],
  templateUrl: './user-list.html',
  styleUrl: './user-list.scss'
})
export class UserList {
  users = signal<User[]>([]);
  rowData = this.users;

  defaultColDef: ColDef = {
    flex: 1,
    sortable: true,
    filter: true,
    floatingFilter: true,
    suppressFloatingFilterButton: true,
    suppressHeaderFilterButton: true
  };


  columnDefs: ColDef<User>[] = [
    {
      field: 'id',
      headerName: 'ID',
      width: 20
    },
    {
      field: 'name',
      headerName: 'Name'
    },
    {
      field: 'email',
      headerName: 'Email'
    },
    {
      headerName: 'Actions',
      valueGetter: () => null,
      sortable: false,
      filter: false,
      width: 40, // Fixed width
      cellRenderer: (params: any) => {
        const button = document.createElement('button');
        button.classList.add('btn', 'btn-primary');
        button.innerText = 'View';
        button.addEventListener('click', () => {
          // Call your function here
          params.context.componentParent.goToDetail(params.data.id);
        });
        return button;
      }
    }
  ];

  gridOptions: GridOptions<any> = {
    columnDefs: this.columnDefs,
    defaultColDef: this.defaultColDef,
    rowSelection: 'single',
    pagination: true,
    onGridReady: this.onGridReady.bind(this),
    onSortChanged: this.onSortChanged.bind(this),
    onPaginationChanged: this.onPaginationChanged.bind(this),
    context: {
      componentParent: this // Pass the parent component to the grid context
    }
  }


  searchParams = {
    pageNumber: 1,
    pageSize: 20,
    sortOrders: {
    },
    searchFilters: {
    }
  };
  gridApi: any;

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit() {
    this.fetchData();
  }

  fetchData() {
    this.userService.getUsers(this.searchParams)
      .subscribe(pageResult => {
        this.users.set(pageResult.items);
      });
  }

  goToDetail(id: number) {
    this.router.navigate(['/users', id]);
  }

  onGridReady(params: GridReadyEvent) {
    params.api.sizeColumnsToFit();
    this.gridApi = params.api;
  }

  onSortChanged(params: any) {
  }

  onPaginationChanged(event: PaginationChangedEvent) {
    if (!event.api) return;
    if (event.newPage) {
      this.searchParams = { ...this.searchParams, pageNumber: event.api.paginationGetCurrentPage() };
      this.searchParams = { ...this.searchParams, pageSize: event.api.paginationGetPageSize() };
      this.fetchData();
    }
  }
}
