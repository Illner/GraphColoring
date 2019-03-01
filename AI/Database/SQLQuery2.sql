Use GraphColoring

select *
from GraphColoringGraph

select *
from GraphColoringGraph
Where ID_Graph = 54102

select count(*)
from GraphColoringBest
--Group by ID_GraphColoringAlgorithm

select ((CAST(count(*) as float) * 100) / 63407), Name, Algorithm.ID_GraphColoringAlgorithm, count(*)
from GraphColoringBest as Best
	join GraphColoringAlgorithm as Algorithm
		on Best.ID_GraphColoringAlgorithm = Algorithm.ID_GraphColoringAlgorithm
Group by Best.ID_GraphColoringAlgorithm, Algorithm.Name, Algorithm.ID_GraphColoringAlgorithm
order by ID_GraphColoringAlgorithm

select Core.ID_GraphColoringAlgorithm, Colors
			from GraphColoringCore as Core
				left join GraphColoringAlgorithm as Algorithm
					on Core.ID_GraphColoringAlgorithm = Algorithm.ID_GraphColoringAlgorithm
			where ID_Graph = 54103
			Order by Colors, Algorithm.TimeComplexity