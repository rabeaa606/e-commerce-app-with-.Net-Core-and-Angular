import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss'],
})
export class PagerComponent implements OnInit {
  @Input() totalCount: number;
  @Input() pageSize: number;
  @Output() pageChenged = new EventEmitter<number>();

  constructor() {}

  ngOnInit(): void {}
  onPagerChanged(event: any) {
    this.pageChenged.emit(event.page);
  }
}
