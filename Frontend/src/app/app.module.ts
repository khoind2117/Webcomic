import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { InfoTruyenRoutingModule } from './info-truyen/info-truyen-routing.module';
import { InfoTruyenComponent } from './info-truyen/info-truyen.component';
import { HomeComponent } from './home/home.component';
import { DanhmucComponent } from './danhmuc/danhmuc.component';
import { DMPhieuLuuComponent } from './dmphieu-luu/dmphieu-luu.component';
import { ChinhSachComponent } from './chinh-sach/chinh-sach.component';
import { DangNhapComponent } from './dang-nhap/dang-nhap.component';
import { DocTruyenComponent } from './doc-truyen/doc-truyen.component';
import { DangKiComponent } from './dang-ki/dang-ki.component';
import { ThongTinUserComponent } from './thong-tin-user/thong-tin-user.component';
import { DangTruyenComponent } from './dang-truyen/dang-truyen.component';
import { SuaTruyenComponent } from './sua-truyen/sua-truyen.component';


@NgModule({
  declarations: [
    AppComponent,

    InfoTruyenComponent,
    HomeComponent,
    DanhmucComponent,
    DMPhieuLuuComponent,
    ChinhSachComponent,
    DangNhapComponent,
    DocTruyenComponent,
    DangKiComponent,
    ThongTinUserComponent,
    DangTruyenComponent,
    SuaTruyenComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
