<template>
    <div>
        <LoadingSpinner v-if="isLoading" />

        <div class="container" v-if="!isLoading" id="grid">
            <div class="row" v-for="(gridRow, rowIndex) in gridData.slice().reverse()" :key="rowIndex">
                <div class="col cell">
                    {{ (gridSize.y - 1) - rowIndex }}
                </div>
                <div class="col cell rover-cell" v-for="(gridCell, cellIndex) in gridRow" :key="cellIndex">
                    <div v-if="hasRover(gridCell)" :class="{ 'has-rover': hasRover(gridCell) }">
                        Rover - {{ gridCell.CurrentBearing }} - X: {{ gridCell.CurrentPosition.X }} Y: {{
                            gridCell.CurrentPosition.Y
                        }} - Facing {{ getCardinalBearing(gridCell) }}

                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col cell"></div>
                <div class="col cell" v-for="(size, index) in parseInt(gridSize.x)" :key="index">
                    {{ parseInt(index) }}
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import LoadingSpinner from "./LoadingSpinner.vue"

const CARDINAL_BEARINGS = {
    North: 0,
    NorthEast: 1,
    East: 2,
    SouthEast: 3,
    South: 4,
    SouthWest: 5,
    West: 6,
    NorthWest: 7
}

export default {
    name: "PlateauRoverPositions",
    props: {
        gridSize: { required: true, type: Object },
        isLoading: { required: true, type: Boolean, default: true },
        gridData: {}
    },
    methods: {
        hasRover(gridCell) {
            return gridCell !== null;
        },
        getCardinalBearing(gridCell) {
            switch (gridCell.CardinalBearing) {
                case CARDINAL_BEARINGS.North:
                    return "North";
                case CARDINAL_BEARINGS.East:
                    return "East";
                case CARDINAL_BEARINGS.South:
                    return "South";
                case CARDINAL_BEARINGS.West:
                    return "West";
            }
        }
    },
    components: {
        LoadingSpinner
    }
}
</script>

<style lang="scss">
#grid {
    text-align: left;
}

.cell {
    margin: 2px;
    background-color: #9eedff;
    color: white;
    font-size: 9px;

    &.rover-cell {
        background-color: white;
        color: black;

        >.has-rover {
            border: 1px solid #b8e1ff;
            text-align: center;
        }
    }
}
</style>