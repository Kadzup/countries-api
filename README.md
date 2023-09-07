# Countries API simple project

## Description
Countries API it's a simple project based on ASP.net with one endpoint that can be used for retrieving information about countries. Users can filter countries by name and population, also they can also sort the results by country name asc or desc.

Also, you should be informed that each request is using pagination, by default endpoint will return you 10 records per page, to see how to use each of the arguments please see **Examples** part.

## SQ
<img width="1191" alt="image" src="https://github.com/Kadzup/countries-api/assets/38593867/bc079c7f-39d8-4615-858b-91ca0334782a">

## How to run this project

### Development mode
If you have installed [Visual Studio](https://visualstudio.microsoft.com/downloads/) with .net6 tools, you can simply fetch this repo using [git](https://git-scm.com/), [Github desktop](https://desktop.github.com/), or similar tools and simply launch the .sln file.
After that, you can simply run the **"Build"** command and launch the "countries-api" project.

### User mode

You can download the attached [release](https://github.com/Kadzup/countries-api/releases/tag/release) artifacts and launch the `countries-api` executable.

## Examples

**Get countries**

Route: `countries/getCountries`

Default values:
- `name` - Country name or part of it's name, use `common` for any name
- `population` - Country population in millions, use `0` for any population
- `sort` - Sort countries by name, default sorting is `ascend`, also you can use `descend`
- `count` - count of countries per request, default value is `10`
- `page` - index of page, usually used with `count` for pagination, default page index is `1`

Request:
```
curl -X 'GET' \
  'https://localhost/countries/getCountries' \
  -H 'accept: text/plain'
```
Response:
```
[{"name":{"common":"Afghanistan","official":"Islamic Republic of Afghanistan"},"region":"Asia","capital":["Kabul"],"currencies":{"AFN":{"name":"Afghan afghani","symbol":"؋"}},"languages":{"prs":"Dari","pus":"Pashto","tuk":"Turkmen"},"population":40218234},{"name":{"common":"Åland Islands","official":"Åland Islands"},"region":"Europe","capital":["Mariehamn"],"currencies":{"EUR":{"name":"Euro","symbol":"€"}},"languages":{"swe":"Swedish"},"population":29458},{"name":{"common":"Albania","official":"Republic of Albania"},"region":"Europe","capital":["Tirana"],"currencies":{"ALL":{"name":"Albanian lek","symbol":"L"}},"languages":{"sqi":"Albanian"},"population":2837743},{"name":{"common":"Algeria","official":"People's Democratic Republic of Algeria"},"region":"Africa","capital":["Algiers"],"currencies":{"DZD":{"name":"Algerian dinar","symbol":"د.ج"}},"languages":{"ara":"Arabic"},"population":44700000},{"name":{"common":"American Samoa","official":"American Samoa"},"region":"Oceania","capital":["Pago Pago"],"currencies":{"USD":{"name":"United States dollar","symbol":"$"}},"languages":{"eng":"English","smo":"Samoan"},"population":55197},{"name":{"common":"Andorra","official":"Principality of Andorra"},"region":"Europe","capital":["Andorra la Vella"],"currencies":{"EUR":{"name":"Euro","symbol":"€"}},"languages":{"cat":"Catalan"},"population":77265},{"name":{"common":"Angola","official":"Republic of Angola"},"region":"Africa","capital":["Luanda"],"currencies":{"AOA":{"name":"Angolan kwanza","symbol":"Kz"}},"languages":{"por":"Portuguese"},"population":32866268},{"name":{"common":"Anguilla","official":"Anguilla"},"region":"Americas","capital":["The Valley"],"currencies":{"XCD":{"name":"Eastern Caribbean dollar","symbol":"$"}},"languages":{"eng":"English"},"population":13452},{"name":{"common":"Antarctica","official":"Antarctica"},"region":"Antarctic","capital":[],"currencies":{},"languages":{},"population":1000},{"name":{"common":"Antigua and Barbuda","official":"Antigua and Barbuda"},"region":"Americas","capital":["Saint John's"],"currencies":{"XCD":{"name":"Eastern Caribbean dollar","symbol":"$"}},"languages":{"eng":"English"},"population":97928}]
```

**Get countries by name**

Will return countries where name contains `name`

Route: `countries/getCountries`

Arguments: `name` - part of country name or full name of country

Request:
```
curl -X 'GET' \
  'https://localhost/countries/getCountries?name=uk' \
  -H 'accept: text/plain'
```
Response:
```
[{"name":{"common":"Ukraine","official":"Ukraine"},"region":"Europe","capital":["Kyiv"],"currencies":{"UAH":{"name":"Ukrainian hryvnia","symbol":"₴"}},"languages":{"ukr":"Ukrainian"},"population":44134693}]
```

**Get countries by population**

Will return countries where population is less then `population`

Route: `countries/getCountries`

Arguments: `population` - millions of population (1 = 1 000 000)

Request:
```
curl -X 'GET' \
  'https://localhost/countries/getCountries?population=30' \
  -H 'accept: text/plain'
```
Response:
```
[{"name":{"common":"Åland Islands","official":"Åland Islands"},"region":"Europe","capital":["Mariehamn"],"currencies":{"EUR":{"name":"Euro","symbol":"€"}},"languages":{"swe":"Swedish"},"population":29458},{"name":{"common":"Albania","official":"Republic of Albania"},"region":"Europe","capital":["Tirana"],"currencies":{"ALL":{"name":"Albanian lek","symbol":"L"}},"languages":{"sqi":"Albanian"},"population":2837743},{"name":{"common":"American Samoa","official":"American Samoa"},"region":"Oceania","capital":["Pago Pago"],"currencies":{"USD":{"name":"United States dollar","symbol":"$"}},"languages":{"eng":"English","smo":"Samoan"},"population":55197},{"name":{"common":"Andorra","official":"Principality of Andorra"},"region":"Europe","capital":["Andorra la Vella"],"currencies":{"EUR":{"name":"Euro","symbol":"€"}},"languages":{"cat":"Catalan"},"population":77265},{"name":{"common":"Anguilla","official":"Anguilla"},"region":"Americas","capital":["The Valley"],"currencies":{"XCD":{"name":"Eastern Caribbean dollar","symbol":"$"}},"languages":{"eng":"English"},"population":13452},{"name":{"common":"Antarctica","official":"Antarctica"},"region":"Antarctic","capital":[],"currencies":{},"languages":{},"population":1000},{"name":{"common":"Antigua and Barbuda","official":"Antigua and Barbuda"},"region":"Americas","capital":["Saint John's"],"currencies":{"XCD":{"name":"Eastern Caribbean dollar","symbol":"$"}},"languages":{"eng":"English"},"population":97928},{"name":{"common":"Armenia","official":"Republic of Armenia"},"region":"Asia","capital":["Yerevan"],"currencies":{"AMD":{"name":"Armenian dram","symbol":"֏"}},"languages":{"hye":"Armenian"},"population":2963234},{"name":{"common":"Aruba","official":"Aruba"},"region":"Americas","capital":["Oranjestad"],"currencies":{"AWG":{"name":"Aruban florin","symbol":"ƒ"}},"languages":{"nld":"Dutch","pap":"Papiamento"},"population":106766},{"name":{"common":"Australia","official":"Commonwealth of Australia"},"region":"Oceania","capital":["Canberra"],"currencies":{"AUD":{"name":"Australian dollar","symbol":"$"}},"languages":{"eng":"English"},"population":25687041}]
```

**Get countries sorted by name**

Will return countries sorted my it's name

Route: `countries/getCountries`

Arguments: `sort` - can be `ascend` or `descend`

Request:
```
curl -X 'GET' \
  'https://localhost/countries/getCountries?sort=descend' \
  -H 'accept: text/plain'
```
Response:
```
[{"name":{"common":"Zimbabwe","official":"Republic of Zimbabwe"},"region":"Africa","capital":["Harare"],"currencies":{"ZWL":{"name":"Zimbabwean dollar","symbol":"$"}},"languages":{"bwg":"Chibarwe","eng":"English","kck":"Kalanga","khi":"Khoisan","ndc":"Ndau","nde":"Northern Ndebele","nya":"Chewa","sna":"Shona","sot":"Sotho","toi":"Tonga","tsn":"Tswana","tso":"Tsonga","ven":"Venda","xho":"Xhosa","zib":"Zimbabwean Sign Language"},"population":14862927},{"name":{"common":"Zambia","official":"Republic of Zambia"},"region":"Africa","capital":["Lusaka"],"currencies":{"ZMW":{"name":"Zambian kwacha","symbol":"ZK"}},"languages":{"eng":"English"},"population":18383956},{"name":{"common":"Yemen","official":"Republic of Yemen"},"region":"Asia","capital":["Sana'a"],"currencies":{"YER":{"name":"Yemeni rial","symbol":"﷼"}},"languages":{"ara":"Arabic"},"population":29825968},{"name":{"common":"Western Sahara","official":"Sahrawi Arab Democratic Republic"},"region":"Africa","capital":["El Aaiún"],"currencies":{"DZD":{"name":"Algerian dinar","symbol":"دج"},"MAD":{"name":"Moroccan dirham","symbol":"DH"},"MRU":{"name":"Mauritanian ouguiya","symbol":"UM"}},"languages":{"ber":"Berber","mey":"Hassaniya","spa":"Spanish"},"population":510713},{"name":{"common":"Wallis and Futuna","official":"Territory of the Wallis and Futuna Islands"},"region":"Oceania","capital":["Mata-Utu"],"currencies":{"XPF":{"name":"CFP franc","symbol":"₣"}},"languages":{"fra":"French"},"population":11750},{"name":{"common":"Vietnam","official":"Socialist Republic of Vietnam"},"region":"Asia","capital":["Hanoi"],"currencies":{"VND":{"name":"Vietnamese đồng","symbol":"₫"}},"languages":{"vie":"Vietnamese"},"population":97338583},{"name":{"common":"Venezuela","official":"Bolivarian Republic of Venezuela"},"region":"Americas","capital":["Caracas"],"currencies":{"VES":{"name":"Venezuelan bolívar soberano","symbol":"Bs.S."}},"languages":{"spa":"Spanish"},"population":28435943},{"name":{"common":"Vatican City","official":"Vatican City State"},"region":"Europe","capital":["Vatican City"],"currencies":{"EUR":{"name":"Euro","symbol":"€"}},"languages":{"ita":"Italian","lat":"Latin"},"population":451},{"name":{"common":"Vanuatu","official":"Republic of Vanuatu"},"region":"Oceania","capital":["Port Vila"],"currencies":{"VUV":{"name":"Vanuatu vatu","symbol":"Vt"}},"languages":{"bis":"Bislama","eng":"English","fra":"French"},"population":307150},{"name":{"common":"Uzbekistan","official":"Republic of Uzbekistan"},"region":"Asia","capital":["Tashkent"],"currencies":{"UZS":{"name":"Uzbekistani soʻm","symbol":"so'm"}},"languages":{"rus":"Russian","uzb":"Uzbek"},"population":34232050}]
```

**Get countries pagination**

Will return pages with countries data

Route: `countries/getCountries`

Arguments: `count` - items per page; `page` - page index starting from 1

Request:
```
curl -X 'GET' \
  'https://localhost/countries/getCountries?count=3&page=2' \
  -H 'accept: text/plain'
```
Response:
```
[{"name":{"common":"Algeria","official":"People's Democratic Republic of Algeria"},"region":"Africa","capital":["Algiers"],"currencies":{"DZD":{"name":"Algerian dinar","symbol":"د.ج"}},"languages":{"ara":"Arabic"},"population":44700000},{"name":{"common":"American Samoa","official":"American Samoa"},"region":"Oceania","capital":["Pago Pago"],"currencies":{"USD":{"name":"United States dollar","symbol":"$"}},"languages":{"eng":"English","smo":"Samoan"},"population":55197},{"name":{"common":"Andorra","official":"Principality of Andorra"},"region":"Europe","capital":["Andorra la Vella"],"currencies":{"EUR":{"name":"Euro","symbol":"€"}},"languages":{"cat":"Catalan"},"population":77265}]
```

### Complex examples

**Find 5 countries where population is less then 5 mil**

Request:
```
curl -X 'GET' \
  'https://localhost/countries/getCountries?population=7&count=5' \
  -H 'accept: text/plain'
```
Response:
```
[{"name":{"common":"Åland Islands","official":"Åland Islands"},"region":"Europe","capital":["Mariehamn"],"currencies":{"EUR":{"name":"Euro","symbol":"€"}},"languages":{"swe":"Swedish"},"population":29458},{"name":{"common":"Albania","official":"Republic of Albania"},"region":"Europe","capital":["Tirana"],"currencies":{"ALL":{"name":"Albanian lek","symbol":"L"}},"languages":{"sqi":"Albanian"},"population":2837743},{"name":{"common":"American Samoa","official":"American Samoa"},"region":"Oceania","capital":["Pago Pago"],"currencies":{"USD":{"name":"United States dollar","symbol":"$"}},"languages":{"eng":"English","smo":"Samoan"},"population":55197},{"name":{"common":"Andorra","official":"Principality of Andorra"},"region":"Europe","capital":["Andorra la Vella"],"currencies":{"EUR":{"name":"Euro","symbol":"€"}},"languages":{"cat":"Catalan"},"population":77265},{"name":{"common":"Anguilla","official":"Anguilla"},"region":"Americas","capital":["The Valley"],"currencies":{"XCD":{"name":"Eastern Caribbean dollar","symbol":"$"}},"languages":{"eng":"English"},"population":13452}]
```

**Find countries with name containing 'un' and sort them des**

Request:
```
curl -X 'GET' \
  'https://localhost/countries/getCountries?name=un&sort=descend' \
  -H 'accept: text/plain'
```
Response:
```
[{"name":{"common":"Wallis and Futuna","official":"Territory of the Wallis and Futuna Islands"},"region":"Oceania","capital":["Mata-Utu"],"currencies":{"XPF":{"name":"CFP franc","symbol":"₣"}},"languages":{"fra":"French"},"population":11750},{"name":{"common":"United States Virgin Islands","official":"Virgin Islands of the United States"},"region":"Americas","capital":["Charlotte Amalie"],"currencies":{"USD":{"name":"United States dollar","symbol":"$"}},"languages":{"eng":"English"},"population":106290},{"name":{"common":"United States Minor Outlying Islands","official":"United States Minor Outlying Islands"},"region":"Americas","capital":["Washington DC"],"currencies":{"USD":{"name":"United States dollar","symbol":"$"}},"languages":{"eng":"English"},"population":300},{"name":{"common":"United States","official":"United States of America"},"region":"Americas","capital":["Washington, D.C."],"currencies":{"USD":{"name":"United States dollar","symbol":"$"}},"languages":{"eng":"English"},"population":329484123},{"name":{"common":"United Kingdom","official":"United Kingdom of Great Britain and Northern Ireland"},"region":"Europe","capital":["London"],"currencies":{"GBP":{"name":"British pound","symbol":"£"}},"languages":{"eng":"English"},"population":67215293},{"name":{"common":"United Arab Emirates","official":"United Arab Emirates"},"region":"Asia","capital":["Abu Dhabi"],"currencies":{"AED":{"name":"United Arab Emirates dirham","symbol":"د.إ"}},"languages":{"ara":"Arabic"},"population":9890400},{"name":{"common":"Tunisia","official":"Tunisian Republic"},"region":"Africa","capital":["Tunis"],"currencies":{"TND":{"name":"Tunisian dinar","symbol":"د.ت"}},"languages":{"ara":"Arabic"},"population":11818618},{"name":{"common":"Tanzania","official":"United Republic of Tanzania"},"region":"Africa","capital":["Dodoma"],"currencies":{"TZS":{"name":"Tanzanian shilling","symbol":"Sh"}},"languages":{"eng":"English","swa":"Swahili"},"population":59734213},{"name":{"common":"Saint Helena, Ascension and Tristan da Cunha","official":"Saint Helena, Ascension and Tristan da Cunha"},"region":"Africa","capital":["Jamestown"],"currencies":{"GBP":{"name":"Pound sterling","symbol":"£"},"SHP":{"name":"Saint Helena pound","symbol":"£"}},"languages":{"eng":"English"},"population":53192},{"name":{"common":"Réunion","official":"Réunion Island"},"region":"Africa","capital":["Saint-Denis"],"currencies":{"EUR":{"name":"Euro","symbol":"€"}},"languages":{"fra":"French"},"population":840974}]
```

**Find 3 countries with name containing 'li', sort them asc where population is less then 20 mil**

Request:
```
curl -X 'GET' \
  'https://localhost/countries/getCountries?name=li&sort=ascend&population=20&count=3' \
  -H 'accept: text/plain'
```
Response:
```
[{"name":{"common":"Albania","official":"Republic of Albania"},"region":"Europe","capital":["Tirana"],"currencies":{"ALL":{"name":"Albanian lek","symbol":"L"}},"languages":{"sqi":"Albanian"},"population":2837743},{"name":{"common":"Andorra","official":"Principality of Andorra"},"region":"Europe","capital":["Andorra la Vella"],"currencies":{"EUR":{"name":"Euro","symbol":"€"}},"languages":{"cat":"Catalan"},"population":77265},{"name":{"common":"Armenia","official":"Republic of Armenia"},"region":"Asia","capital":["Yerevan"],"currencies":{"AMD":{"name":"Armenian dram","symbol":"֏"}},"languages":{"hye":"Armenian"},"population":2963234}]
```

**Get 5 countries from the second page of sorted asc by name countries where population is less then 80 mil**

Request:
```
curl -X 'GET' \
  'https://localhost/countries/getCountries?sort=ascend&population=20&count=5&page=2' \
  -H 'accept: text/plain'
```
Response:
```
[{"name":{"common":"Antarctica","official":"Antarctica"},"region":"Antarctic","capital":[],"currencies":{},"languages":{},"population":1000},{"name":{"common":"Antigua and Barbuda","official":"Antigua and Barbuda"},"region":"Americas","capital":["Saint John's"],"currencies":{"XCD":{"name":"Eastern Caribbean dollar","symbol":"$"}},"languages":{"eng":"English"},"population":97928},{"name":{"common":"Armenia","official":"Republic of Armenia"},"region":"Asia","capital":["Yerevan"],"currencies":{"AMD":{"name":"Armenian dram","symbol":"֏"}},"languages":{"hye":"Armenian"},"population":2963234},{"name":{"common":"Aruba","official":"Aruba"},"region":"Americas","capital":["Oranjestad"],"currencies":{"AWG":{"name":"Aruban florin","symbol":"ƒ"}},"languages":{"nld":"Dutch","pap":"Papiamento"},"population":106766},{"name":{"common":"Austria","official":"Republic of Austria"},"region":"Europe","capital":["Vienna"],"currencies":{"EUR":{"name":"Euro","symbol":"€"}},"languages":{"de":"German"},"population":8917205}]
```
