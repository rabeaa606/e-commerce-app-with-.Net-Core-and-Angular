import { ShopParams } from './../../models/shopParams';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-pagin-header',
  templateUrl: './pagin-header.component.html',
  styleUrls: ['./pagin-header.component.scss'],
})
export class PaginHeaderComponent implements OnInit {
  @Input() totalCount: number;
  @Input() pageSize: number;
  @Input() pageNumber: number;

  constructor() {}

  ngOnInit(): void {}
}
