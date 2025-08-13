import { Component, OnInit, signal } from '@angular/core';
import { User } from '../../models/user.model';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../../services/user.service';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-user-detail',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './user-detail.html',
  styleUrl: './user-detail.scss'
})
export class UserDetail implements OnInit {
  user = signal<User>(null);
  editMode = signal(false);

  userForm = new FormGroup({
    name: new FormControl('', { nonNullable: true, validators: [Validators.required] }),
    email: new FormControl('', { nonNullable: true, validators: [Validators.required, Validators.email] })
  });

  constructor(private router: Router, private route: ActivatedRoute, private userService: UserService) { }

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (id) {
      this.userService.getUser(id).subscribe(data => {
        this.user.set(data);
        this.userForm.patchValue({
          name: data.name,
          email: data.email
        });
      });
    }
  }

  toggleEdit() {
    this.editMode.update(v => !v);
  }

  saveChanges() {
    if (this.userForm.valid) {
      const updatedData = this.userForm.getRawValue(); 
      this.user.update(u => ({ ...u, ...updatedData }));
      this.editMode.set(false);
    }
  }

  goToIndex() {
    this.router.navigate(['/users']);
  }
}
