<div dir="rtl">

# ONVIF

## معرفی

[Open Network Video Interface Forum](https://en.wikipedia.org/wiki/ONVIF)
یک انجمن با هدف آسان ساختن توسعه و استفاده از یک استاندارد جهانی و باز برای ارتباط با محصولات فیزیکی حفاظتی بر پایه IP است.

به عبارت دیگر، ساخت یک استاندارد برای مشخص کردن چگونگی ارتباط محصولات حفاظت ویدئویی و دیگر دستگاه‌های حفاظتی با یکدیگر.

در ONVIF
از وب‌سرویس‌ها برای ارتباط و کنترل دستگاه‌‌ها و از پروتکل‌های RTP/RTSP برای استریمینگ، تشخصی حرکت و صدا و از H.264 برای فشرده سازی استفاده می‌شود.

## [Profile S](https://www.onvif.org/profiles/profile-s/)

این پروفایل اولین پروفایلی است که ONVIF عرضه کرده است.
این پروفایل نحوه ارتباط با IP Camera ها را مشخص می‌کند که شامل ویژگی‌های زیر است:

- ‌استریمینگ تصویر و صدا
- کنترل استریمینگ
- امکانات Pan, Tilt, Zoom
- کنترل خروجی رله

که معمولا این پروفایل رابط بین IPCamera ها 
و VMSها است.

VMS: Video Management System

برای اطلاعات بیشتر به [سند مشخصات این پروفایل](https://www.onvif.org/wp-content/uploads/2017/01/ONVIF_Profile_-S_Specification_v1-1-1.pdf) مراجعه شود.

### Video Streaming

هر دستگاه باید حداقل یک Media Profile برای ویدئو استریمینگ داشته باشد.
و توابع GetProfiles, GetStreamUri را در سرویس Media پیاده‌سازی کرده باشد.

برای اطلاعات بیشتر به [سند مشخصات استریمینگ](www.onvif.org/onvif/specs/stream/ONVIF-Streaming-Spec-v211.pdf) مراجعه شود.

### Encoder Config

 از دستگاه‌هایی که ممکن است از این پروفایل استفاده کند Media Encoderها هستند.
که حداکثر تعداد استریم‌های همزمان را با تابع GetGuaranteedNumberOfVideoEncoderInstances برمی‌گرداند.

از تابع GetVideoEncoderConfiguration برای دریافت تنظیمات فعلی و از تابع SetVideoEncoderConfiguration برای تغییر تنظیمات انکدر استفاده می‌شود.

### Video Source

یک وید‌ئو Encode نشده.
این ساختار شامل Video, Framerate و Imaging Settings است.

در سرویس مدیا تعدادی تابع برای کار با Video Source وجود دارد.

برای مشاهده دیگر توابع و پارامترهای آن به [سند مشخصات سرویس مدیا](https://www.onvif.org/specs/srv/media/ONVIF-Media-Service-Spec-v240.pdf) مراجعه شود.

### PTZ

- Pan: چرخش به طرفین
- Tilt: چرخش به بالا و پایین
- Zoom: بزرگنمایی

این پروفایل امکان کار با دستگاه(دوربین)های PTZ را دارد. اما پیاده سازی سرویس‌های آن در دستگاه‌ها اختیاری است.

توابع این قابلیت در سرویس‌های [مدیا](https://www.onvif.org/specs/srv/media/ONVIF-Media-Service-Spec-v240.pdf) و [PTZ](https://www.onvif.org/specs/srv/ptz/ONVIF-PTZ-Service-Spec-v221.pdf) وجود دارد.

### شبیه‌ساز برای تست

یکی از راه‌های تست پروتکل و برنامه‌های نوشته شده برای آن استفاده از یک شبیه‌ساز ONVIF است.

برای این منظور می‌توانیم از [این فایل‌ها](http://www.lingodigit.com/onvif/ONVIF-Emulator-20160529.tar.xz)
 در برنامه Virtual Box استفاده کنیم.

 همچنین می‌توان از
[این برنامه](http://www.happytimesoft.com/products/multi-onvif-server/index.html)
 به این منظور استفاده کرد.

### برنامه‌های نمونه

در این برنامه‌ها قصد داریم یک کلاینت برای استفاده از تابع GetSystemDateAndTime از سرویس Device پیاده‌سازی کنیم.

#### Simple

این برنامه ساده ترین روش برای انجام این کار، یعنی اضافه کردن Service Reference به پروژه با استفاده از فایل [وب‌ سرویس](https://www.onvif.org/ver10/device/wsdl/devicemgmt.wsdl) و فراخوانی تابع از کدهای Generate شده را انجام می‌دهد.

* تنظیمات Endpoint به Settings.config منتقل شده است که معمولا کنار پروژه و در گیت قرار نمی‌گیرد، اما برای راحتی کار در پروژه اضافه شده است.

#### OnvifApplication

این برنامه با استفاده از دو پروژه ONVIF.Framework و ONVIF.Library تابع GetSystemDateAndTime را فراخوانی می‌کند.
 
 پروژه ONVIF.Framework برای مخفی سازی پیاده سازی فراخوانی توابع و ایجاد یک ساختار منظم استفاده شده است.
 پروژه ONVIF.Library پیاده سازی سرویس‌های ONVIF را بر عهده دارد.

* تنظیمات Endpoint به Settings.config منتقل شده است که معمولا کنار پروژه و در گیت قرار نمی‌گیرد، اما برای راحتی کار در پروژه اضافه شده است.

</div>