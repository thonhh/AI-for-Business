Chạy Okie - mã thô
https://github.com/benselby/CS686
Homework Assignment 1: Search Algorithms
• B: basic backtracking search (no forward checking, random variable order and random value order)

• B+FC: backtracking search with forward checking (random variable order and random value order)

• B+FC+H: backtracking search with forward checking and the 3 heuristics to order variables and values
(break any remaining ties in the order of the variables and values at random)

Error: 
1. Thêm tham số vào file thực thi trong python:
https://stackoverflow.com/questions/33102272/pycharm-and-sys-argv-arguments

2. Lỗi: #domain = range(1,10) --> domain = list(range(1,10))
https://stackoverflow.com/questions/22437297/python-3-range-append-returns-error-range-object-has-no-attribute-appen
   
   dòng 358: [remaining_values.append( range(1,10) ) for i in range(81) ]
   
   ->  [remaining_values.append( list(range(1,10)) ) for i in range(81) ]
    
3. TypeError: list indices must be integers, not float
--> thay phép chia: / thành //
https://stackoverflow.com/questions/13355816/typeerror-list-indices-must-be-integers-not-float