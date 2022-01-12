# ItemsExperimental

Simple windows console application to extract information needed to create the itemsexperimental.txt file for RustAdmin.

## Description

RustAdmin needs a text file containing items that may be spawned by an admin. This text file, itemsexperimental.txt, should be be checked and updated as neeeded when a new Rust Dedicated Server is released. This console application should allow for a quick and easy method to generate this file on demand.

## Getting Started

### Dependencies

* No dependancies required.

### Installing

* ItemsExperimental may be placed anywhere.

### Executing program

* ItemsExperimental option "path\to\rust\dedicated\server"
* example: 
```
ItemsExperimental -extract "C:\rustserver"
```

## Help

Help can be seen here:
```
ItemsExperimental -help
```

## Known issues

* path extraction
* some items are missing some information, need to fill with defaults.
* Not for RELEASE as of yet. (Getting close...)


## Authors

CatMeat

## Version History

* 0.2
    * Various bug fixes and optimizations
    * See [commit change]() or See [release history]()
* 0.1
    * Initial Release

## License

This project is licensed under the MIT License - see the LICENSE.md file for details

## Acknowledgments

* [RustAdmin](https://www.rustadmin.com/)