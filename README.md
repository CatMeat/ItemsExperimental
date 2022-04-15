# ExtractRustItems

Simple windows console application to extract information needed to create the itemsexperimental.txt file for RustAdmin.

## Description

RustAdmin needs a text file containing items that may be spawned by an admin. This text file, itemsExperimental.txt, should be be checked and updated as neeeded when a new Rust Dedicated Server is released. This console application should allow for a quick and easy method to generate this file on demand.

## Getting Started

### Dependencies

* Newtonsoft.Json.dll

### Installing

* ExtractRustItems may be placed anywhere. itemsExperimental.txt and Changes.txt will be saved to the active folder.

### Executing program

* ExtractRustItems "path\to\rust\dedicated\server"
* example: 
```
ExtractRustItems C:\rustserver
```
if your path contains spaces, surround it with  quotes.
```
ExtractRustItems "C:\rust server"
```


## Known issues

* none reported


## Authors

CatMeat

## Version History

* 0.3
    * Major overhaul and refactor
* 0.2
    * Various bug fixes and optimizations
    * See [commit change]() or See [release history]()
* 0.1
    * Initial Release

## License

This project is licensed under the MIT License - see the LICENSE.md file for details

## Acknowledgments

* [RustAdmin](https://www.rustadmin.com/)