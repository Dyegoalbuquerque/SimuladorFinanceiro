import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MatStepperModule} from '@angular/material/stepper';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { MaterialModule} from './material-module';
import { HttpClientModule, /* other http imports */ } from "@angular/common/http";
import { ListComponent } from './components/list/list.component';
import { DetailComponent } from './components/detail/detail.component';
import { DialogOverviewExampleDialog } from './components/list/list.component';

@NgModule({
  declarations: [
    AppComponent, ListComponent, DetailComponent, DialogOverviewExampleDialog
  ],
  
  entryComponents: [DialogOverviewExampleDialog],
  imports: [
    BrowserModule,
    AppRoutingModule, MatStepperModule, FormsModule,
    ReactiveFormsModule, MaterialModule,HttpClientModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
